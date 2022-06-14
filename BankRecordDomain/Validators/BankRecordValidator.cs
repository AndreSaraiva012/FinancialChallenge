using BankRecordDomain.Entities;
using FluentValidation;

namespace BankRecordDomain.Validators
{
    public class BankRecordValidator : AbstractValidator<BankRecord>
    {
        public BankRecordValidator()
        {
            RuleFor(x => x.Origin)
                .IsInEnum().WithMessage("The Origin selected Invalid....");

            RuleFor(x => x.OriginId)
                .Null().When(x => x.Origin == null).WithMessage("OriginId must be null....");

            RuleFor(x => x.Type)
                .NotNull().WithMessage("This field is required....")
                .IsInEnum().WithMessage("The Type selected Invalid....");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("The Description field is required....");

            RuleFor(x => x.Amount)
                .NotNull().WithMessage("The Amount field is required....")
                .LessThan(0).WithMessage("Payment option must have a negative Amount....")
                .When(x => x.Type == Type.Payment, ApplyConditionTo.CurrentValidator)
                .GreaterThan(0).WithMessage("Payment option must have a positive Amount....")
                .When(x => x.Type == Type.Receive, ApplyConditionTo.CurrentValidator);
        }
    }
}