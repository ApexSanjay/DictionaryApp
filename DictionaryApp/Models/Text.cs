using System.ComponentModel.DataAnnotations;

namespace DictionaryApp.Models
{
    public class Text
    {
        public int Id { get; set; }
        [RegularExpression(@"^[\u0900-\u097F]+$", ErrorMessage = "Only Nepalese characters are allowed.")]
        public required string Word { get; set; }
        [RegularExpression(@"^[a-z\u00C0-\u02AF\u1E00-\u1EFF]+$", ErrorMessage = "Only letters or diacritic letters are allowed.")]
        public required string Pronunciation { get; set; }
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only A-Z characters are allowed.")]
        public required string Definition { get; set; }

    }
}
