using FluentValidation.Results;
using MediatR;


namespace Domain.Base.Messages
{
    public abstract class Command<TResponse> : Message, IRequest<TResponse>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; } // Coleção de erros de validação.

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
