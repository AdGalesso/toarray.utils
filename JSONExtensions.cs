using Newtonsoft.Json;

namespace toarray.utils
{
    public static class JsonExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="handlingAll"></param>
        /// <param name="handlingObj"></param>
        /// <param name="ignoreNull"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, bool handlingAll = true, bool handlingObj = false, bool ignoreNull = true)
        {
            var settings = new JsonSerializerSettings();

            if (handlingAll || handlingObj)
                settings.TypeNameHandling = handlingAll ? TypeNameHandling.All : handlingObj ? TypeNameHandling.Objects : TypeNameHandling.None;
            else
                settings.TypeNameHandling = TypeNameHandling.None;

            settings.NullValueHandling = ignoreNull ? NullValueHandling.Ignore : NullValueHandling.Include;

            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}
