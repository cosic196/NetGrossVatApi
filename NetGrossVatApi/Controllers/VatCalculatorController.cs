using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NetGrossVatApi.Core;
using NetGrossVatApi.Core.DataModels;

namespace NetGrossVatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatCalculatorController : ControllerBase
    {
        private IVatCalculator _vatCalculator;
        private IVatCalculatorInputParser _inputParser;

        public VatCalculatorController(IVatCalculator vatCalculator, IVatCalculatorInputParser inputParser)
        {
            _vatCalculator = vatCalculator;
            _inputParser = inputParser;
        }


        [HttpGet]
        public VatCalculatorResult GetCalculationResult([FromQuery]IDictionary<string, string> input)
        {
            VatCalculatorInput parsedInput;
            try
            {
                parsedInput = _inputParser.Parse(input);
            }
            catch(Exception ex)
            {
                return new VatCalculatorResult(ex.Message);
            }
            return _vatCalculator.Calculate(parsedInput);
        }
    }
}