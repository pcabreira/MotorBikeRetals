using System;

namespace MotorBikeRetals.Core.Entities
{
    public class UserDetails
    {
        public UserDetails(string cnpj,
                           DateTime birthDate,
                           string numberCNH,
                           string typeCNH)
        {
            CNPJ = cnpj;
            BirthDate = birthDate;
            NumberCNH = numberCNH;
            TypeCNH = typeCNH;
        }

        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string NumberCNH { get; set; }
        public string TypeCNH { get; set; }
        public string ImageURL { get; set; }

        public void Update(string imageURL)
        {
            ImageURL = imageURL;
        }
    }
}
