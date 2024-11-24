using System.ComponentModel.DataAnnotations;

namespace world.Model
{
	
	public class Country
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }

		public string ShortName {  get; set; }
        public string CountryCode { get; set; }
    }
}
