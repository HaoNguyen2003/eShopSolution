using CloudinaryDotNet;
using dotenv.net;

namespace eShopSolution.cloudinaryManagerFile.config
{
    public class CloudinaryConfig
    {
        private static readonly Cloudinary _cloudinary;
        static CloudinaryConfig()
        {
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            string cloudinaryUrl = @"cloudinary://721154231843134:1KwHgG2sdrczy4fCUfic-hKCtos@dqtnqk8fq";
            _cloudinary = new Cloudinary(cloudinaryUrl);
            _cloudinary.Api.Secure = true;
        }
        public static Cloudinary GetCloudinary()
        {
            return _cloudinary;
        }
    }
}
