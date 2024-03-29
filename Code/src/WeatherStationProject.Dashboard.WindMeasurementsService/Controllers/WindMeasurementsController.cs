﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherStationProject.Dashboard.Core.DateTime;
using WeatherStationProject.Dashboard.Data.Validations;
using WeatherStationProject.Dashboard.WindMeasurementsService.Services;
using WeatherStationProject.Dashboard.WindMeasurementsService.ViewModel;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/wind-measurements")]
    public class WindMeasurementsController : ControllerBase
    {
        private readonly IWindMeasurementsService _windMeasurementsService;

        public WindMeasurementsController(IWindMeasurementsService windMeasurementsService)
        {
            _windMeasurementsService = windMeasurementsService;
        }

        [HttpGet("last")]
        public async Task<ActionResult<WindMeasurementsDto>> LastMeasurement()
        {
            var last = await _windMeasurementsService.GetLastWindMeasurements();

            if (null == last) return NotFound();

            return WindMeasurementsDto.FromEntity(last);
        }

        [HttpGet("gust-in-time/{minutes}")]
        public async Task<ActionResult<WindMeasurementsDto>> GustInTime([Required] [Range(15, 60)] int minutes)
        {
            var gust = await _windMeasurementsService.GetGustInTime(minutes);

            if (null == gust) return NotFound();

            return WindMeasurementsDto.FromEntity(gust);
        }
        
        [HttpGet("historical")]
        public async Task<ActionResult<HistoricalDataDto>> HistoricalData(
            [Required] DateTime since,
            [Required] DateTime until,
            [Required] [GroupingRange] string grouping,
            [Required] bool includeSummary,
            [Required] bool includeMeasurements)
        {
            var records = await _windMeasurementsService.GetWindMeasurementsBetweenDates(DateTimeConverter.ConvertToUtc(since),
                DateTimeConverter.ConvertToUtc(until));

            if (records.Count == 0) return NotFound();

            return new HistoricalDataDto(records,
                (GroupingValues)Enum.Parse(typeof(GroupingValues), grouping),
                includeSummary,
                includeMeasurements);
        }
    }
}