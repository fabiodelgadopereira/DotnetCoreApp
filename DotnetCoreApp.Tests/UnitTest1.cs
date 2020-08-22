using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using CadastroApp.API.Models;
using Xunit;

namespace DotnetCoreApp.Tests {
    public class UnitTest1 {
        [Fact]
        public void TesteEmailInvalidoModel () {

            var cli = new Cliente {
                Email = "a",
            };
            //ACT
            var listaDeErros = ValidateModel (cli);
            //ASSERT
           Assert.True(listaDeErros.Where(x => x.ErrorMessage.Contains("Invalid")).Count() > 0);
        }

      private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}