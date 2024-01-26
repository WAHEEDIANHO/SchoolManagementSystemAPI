namespace SchoolManagementSystemAPI.Services.Teacher.Extention
{
    public static class WebApplicationExtention
    {
        public static WebApplicationBuilder AddClient(this WebApplicationBuilder builder) {
            builder.Services.AddHttpClient("UserAPI", opt => opt.BaseAddress = new Uri(builder.Configuration["ServiceUrl:UserAPI"]));
            return builder;
        } 
    }
}
