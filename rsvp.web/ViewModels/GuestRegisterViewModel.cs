using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace rsvp.web.ViewModels
{
    [Validator(typeof(GuestRegisterViewModelValidator))]
    public class GuestRegisterViewModel
    {
        public int Id { get; set; }

        public string PromoCode { get; set; }

        //[Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        //        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        //        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        //        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        //        [Required(ErrorMessage = "State is required.")]
        public string State { get; set; }

        //        [Required(ErrorMessage = "Zipcode is required.")]
        public string Zipcode { get; set; }

        public string Comment { get; set; }

        public int? TicketCount { get; set; }

        public int? TicketAllowance { get; set; }

        //[Required(ErrorMessage = "Please select Yes or No if you are attending")]
        public bool? IsAttending { get; set; }

        public DateTime? ResponseDate { get; set; }

        public int? EventId { get; set; }

        public string EventName { get; set; }

        public void Validate()
        {
        }
    }

    public class GuestRegisterViewModelValidator : AbstractValidator<GuestRegisterViewModel>
    {
        public GuestRegisterViewModelValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Name is required.");
            RuleFor(x => x.Address).NotNull().WithMessage("Address is required.");
            RuleFor(x => x.City).NotNull().WithMessage("City is required.");
            RuleFor(x => x.State).NotNull().WithMessage("State is required.");
            RuleFor(x => x.Zipcode).NotNull().WithMessage("Zipcode is required.");
            RuleFor(x => x.Phone).NotNull().WithMessage("Phone is required.");
            RuleFor(x => x.Email).NotNull().WithMessage("Email is required.");
            RuleFor(x => x.IsAttending).NotNull().WithMessage("Please select Yes or No if you are attending.");
        }

    }
}