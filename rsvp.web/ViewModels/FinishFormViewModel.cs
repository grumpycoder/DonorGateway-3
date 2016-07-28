using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;


namespace rsvp.web.ViewModels
{
    public class FinishFormViewModel
    {
        public int EventId { get; set; }

        public string CurrentDate { get; private set; }
        public byte[] Image { get; set; }
        public string MimeType { get; set; }


        public string HeaderText { get; set; }
        public string BodyText { get; set; }
        public string FooterText { get; set; }
        public string FAQText { get; set; }
        public string YesText { get; set; }
        public string NoText { get; set; }
        public string WaitText { get; set; }

        public string Name { get; set; }
        public bool IsAttending { get; set; }
        public bool IsWaiting { get; set; }
        public string Email { get; set; }
        public string InsideSalutation { get; set; }
        public string EmailSalutation { get; set; }

        public string DisplayName { get; set; }
        public string Speaker { get; set; }
        public string Venue { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string VenueOpenDate { get; set; }

        public RegisterFormViewModel RegisterForm { get; set; }
        public EventViewModel EventViewModel { get; set; }

        public FinishFormViewModel()
        {
            CurrentDate = DateTime.Today.ToString("d");
        }
        public void ProcessMessages()
        {
            var properties = ((Type)typeof(FinishFormViewModel)).GetProperties().Where(p => p.PropertyType == typeof(string));
            foreach (var prop in properties)
            {
                ProcessField(prop);
            }
        }

        private void ProcessField(PropertyInfo field)
        {
            if (field.GetValue(this, null) == null) return;

            var text = field.GetValue(this, null).ToString();

            var propertyInfos = ((Type)typeof(FinishFormViewModel)).GetProperties().Where(p => p.PropertyType == typeof(string));

            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetValue(this, null) == null) continue;
                var propValue = propertyInfo.GetValue(this, null).ToString();

                if (string.IsNullOrWhiteSpace(propValue)) continue;
                text = ReplaceText(text, propertyInfo.Name, propValue);
            }

            field.SetValue(this, Convert.ChangeType(text, field.PropertyType), null);

        }


        private static string ReplaceText(string stringToReplace, string fieldName, string fieldValue)
        {

            var pattern = "@{" + fieldName + "}";

            var regex = new Regex(pattern);
            var matches = regex.Matches(stringToReplace);

            return matches.Replace(stringToReplace, fieldValue);

        }
    }

    public static class RegexExtensions
    {
        public static string Replace(this MatchCollection matches, string source, string replacement)
        {
            foreach (var match in matches.Cast<Match>())
            {
                source = match.Replace(source, replacement);
            }
            return source;
        }
        public static string Replace(this Match match, string source, string replacement)
        {
            return source.Substring(0, match.Index) + replacement + source.Substring(match.Index + match.Length);
        }
    }
}