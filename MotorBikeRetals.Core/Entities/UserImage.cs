namespace MotorBikeRetals.Core.Entities
{
    public class UserImage : BaseEntity
    {
        public UserImage(string idUSer,
                         string file)
        {
            IdUSer = idUSer;
            File = file;
        }

        public string IdUSer { get; set; }
        public string File { get; set; }
    }
}
