﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.Dto;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewerController: Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IMapper _mapper;

        public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
        {
            _reviewerRepository = reviewerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Reviewer>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewers()
        {
            var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());
            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(reviewers);
        }

        [HttpGet("{reviewerId}")]
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                NotFound();
            var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(reviewerId));
            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(reviewer);
        }

        [HttpGet("{reviewerId}/reviews")]
        [ProducesResponseType(200, Type =typeof(List<Review>))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerRepository.ReviewerExists(reviewerId))
                NotFound();
            var reviewer = _mapper.Map<List<ReviewDto>>(_reviewerRepository.GetReviewsByReviewer(reviewerId));
            if (!ModelState.IsValid)
                BadRequest(ModelState);
            return Ok(reviewer);
        }
    }
}
