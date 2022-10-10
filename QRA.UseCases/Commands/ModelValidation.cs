using QRA.Entities.Models;
using QRA.UseCases.DTOs;
using QRA.UseCases.Helpers;

namespace QRA.UseCases.commands
{
    public class ModelValidation : IModelValidation
    {

        public bool isFieldsValid(RegisterDTO register)
        {


            return true;
        }
    }
}
