using System;

namespace Project.Configurations.Preconditions
{
    public class PreconditionsHandler
    {
        public void AssesThatIsMet<T>(Func<T, bool> evaluationFunc, T evaluatedParameter, Exception exception)
        {
            bool resultOfEvaluation = evaluationFunc(evaluatedParameter);

            if (resultOfEvaluation == false)
            {
                throw exception;
            }
        }
    }
}
