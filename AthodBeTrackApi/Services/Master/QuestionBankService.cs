using AthodBeTrackApi.Data;
using AthodBeTrackApi.Models;
using AthodBeTrackApi.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace AthodBeTrackApi.Services
{
    public class QuestionBankService : IQuestionBankService
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository _genericRepository;
        private readonly IMasterRepository _masterRepository;
        public QuestionBankService(IMapper mapper, IGenericRepository genericRepository, IMasterRepository masterRepository)
        {
            _mapper = mapper;
            _genericRepository = genericRepository;
            _masterRepository = masterRepository;
        }

        public async Task<List<GetQuestionBank_Result>> GetAsync(int? questionId)
        {
            try
            {
                var dataTable = await Task.FromResult(_masterRepository.GetQuestionBank(questionId));
                return ExtensionMethods.ConvertToList<GetQuestionBank_Result>(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<QuestionBankModel> GetByIdAsync(int id)
        {
            try
            {
                var questionChoice = await _genericRepository.GetByIDAsync<QuestionBank>(id);
                return _mapper.Map<QuestionBankModel>(questionChoice);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddAsync(QuestionBankModel model)
        {
            try
            {
                var question = _mapper.Map<QuestionBank>(model);
                question.QuestionType = null;
                question.ReportingFrequencyType = null;
                question.ActivityQuestions = null;
                question.QuestionChoiceMappings = null;
                question.Language = null;
                question.QuestionId = 0;
                var questionId = await _genericRepository.InsertAsync(question);
                if (questionId > 0)
                {
                    // add skip question ids
                    if (model.SkipQuestionIds?.Trim().Length > 0)
                    {
                        foreach (var item in model.SkipQuestionIds.Split(','))
                        {
                            var questionBank = await _genericRepository.GetByIDAsync<QuestionBank>(item);
                            if (questionBank != null)
                            {
                                questionBank.SkipQuestionId = questionId;
                                questionBank.UpdatedOn = model.CreatedOn;
                                questionBank.UpdatedBy = model.CreatedBy;
                                await _genericRepository.UpdateAsync(questionBank);
                            }
                        }
                    }

                    // Insert in QuestionTagMapping
                    if (model.TagIds.Trim().Length > 0)
                    {
                        foreach (var item in model.TagIds.Split(','))
                        {
                            var tagMapping = new QuestionTagMapping
                            {
                                QuestionId = questionId,
                                TagId = Convert.ToInt32(item),
                                IsActive = true,
                                CreatedBy = model.CreatedBy,
                                CreatedOn = model.CreatedOn
                            };
                            await _genericRepository.InsertAsync(tagMapping);
                        }
                    }

                    // insert Data in QuestionChoiceMapping
                    if (model.QuestionChoiceId != null && model.QuestionChoiceId > 0)
                    {
                        var questionChoiceMapping = await _genericRepository.GetFirstOrDefaultAsync<QuestionChoiceMapping>(x => x.QuestionId == model.QuestionId);
                        if (questionChoiceMapping != null)
                        {
                            questionChoiceMapping.QuestionChoiceId = model.QuestionChoiceId;
                            questionChoiceMapping.UpdatedBy = model.CreatedBy;
                            questionChoiceMapping.UpdatedOn = model.CreatedOn;
                            await _genericRepository.UpdateAsync(questionChoiceMapping);
                        }
                        else
                        {
                            var choiceMapping = new QuestionChoiceMapping()
                            {
                                QuestionId = model.QuestionId,
                                QuestionChoiceId = model.QuestionChoiceId,
                                IsActive = true,
                                CreatedOn = model.CreatedOn,
                                CreatedBy = model.CreatedBy
                            };
                            await _genericRepository.InsertAsync(choiceMapping);
                        }
                    }

                    return true;
                }

                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<bool> UpdateAsync(QuestionBankModel request)
        {
            try
            {
                var question = await _genericRepository.GetByIDAsync<QuestionBank>(request.QuestionId);
                question.Question = request.Question;
                question.QuestionTypeId = request.QuestionTypeId;
                //question.ParentQuestionId = request.ParentQuestionId;
                question.Mandatory = request.Mandatory;
                question.Sort = request.Sort;
                question.MaxLength = request.MaxLength;
                question.MinLength = request.MinLength;
                question.Constraint = request.Constraint;
                question.Format = request.Format;
                question.IsFormula = request.IsFormula;
                question.Formula = request.Formula;
                //question.HavingChild = request.HavingChild;
                question.ReportingFrequencyTypeId = request.ReportingFrequencyTypeId;
                question.TimeStampManual = request.TimeStampManual;
                question.Help = request.Help;
                question.PalceHolder = request.PalceHolder;
                question.IsActive = request.IsActive;
                question.UpdatedOn = request.UpdatedOn;
                question.UpdatedBy = request.UpdatedBy;

                question.SkipLogic = request.SkipLogic;
                question.SkipLogicDetail = request.SkipLogicDetail;

                await _genericRepository.UpdateAsync(question);

                // add skip question ids
                if (request.SkipQuestionIds?.Trim().Length > 0)
                {
                    foreach (var item in request.SkipQuestionIds.Split(','))
                    {
                        var questionBank = await _genericRepository.GetByIDAsync<QuestionBank>(item);
                        if (questionBank != null)
                        {
                            questionBank.SkipQuestionId = request.QuestionId;
                            questionBank.UpdatedOn = request.UpdatedOn;
                            questionBank.UpdatedBy = request.UpdatedBy;
                            await _genericRepository.UpdateAsync(questionBank);
                        }
                    }
                }


                await _masterRepository.DeleteQuestionTagMappingAsync(request.QuestionId);

                // Insert in QuestionTagMapping
                if (request.TagIds?.Trim().Length > 0)
                {
                    foreach (var item in request.TagIds.Split(','))
                    {
                        var tagMapping = new QuestionTagMapping
                        {
                            QuestionId = request.QuestionId,
                            TagId = Convert.ToInt32(item),
                            IsActive = true,
                            CreatedBy = request.UpdatedBy ?? 1,
                            CreatedOn = request.UpdatedOn ?? DateTime.Now
                        };
                        await _genericRepository.InsertAsync(tagMapping);
                    }
                }

                // insert Data in QuestionChoiceMapping
                if (request.QuestionChoiceId != null && request.QuestionChoiceId > 0)
                {
                    var questionChoiceMapping = await _genericRepository.GetFirstOrDefaultAsync<QuestionChoiceMapping>(x => x.QuestionId == request.QuestionId);
                    if (questionChoiceMapping != null)
                    {
                        questionChoiceMapping.QuestionChoiceId = request.QuestionChoiceId;
                        questionChoiceMapping.UpdatedBy = request.UpdatedBy;
                        questionChoiceMapping.UpdatedOn = request.UpdatedOn;
                        await _genericRepository.UpdateAsync(questionChoiceMapping);
                    }
                    else
                    {
                        var choiceMapping = new QuestionChoiceMapping()
                        {
                            QuestionId = request.QuestionId,
                            QuestionChoiceId = request.QuestionChoiceId,
                            IsActive = true,
                            CreatedBy = request.UpdatedBy ?? 1,
                            CreatedOn = request.UpdatedOn ?? DateTime.Now
                        };
                        await _genericRepository.InsertAsync(choiceMapping);
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _genericRepository.DeleteAsync<QuestionBank>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
