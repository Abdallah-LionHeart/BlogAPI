using API.Data;
using API.Entities;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class DbInitializer
{
    public static async Task SeedData(BlogContext context, UserManager<Admin> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Ensure database is created
        context.Database.Migrate();

        // Seed Admin User
        var adminRole = "Admin";

        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        var adminUser = new Admin
        {
            UserName = "UmSadam",
            Email = "umSadam@outlook.com",
            FirstName = "Sadam",
            LastName = "Magableh",
            EmailConfirmed = true
        };

        if (await userManager.FindByNameAsync(adminUser.UserName) == null)
        {
            var result = await userManager.CreateAsync(adminUser, "Magableh@Admin2024");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }

        // Seed AppUser data
        if (!await context.AppUsers.AnyAsync())
        {
            var appUser = new AppUser
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Education = "B.Sc. Computer Science",
                Experience = "5 years in software development",
                Position = "Software Engineer",
                Overview = "Experienced software engineer...",
                PhoneNumber = "123-456-7890",
                Age = 30,
                FacebookLink = "http://facebook.com/johndoe",
                TwitterLink = "http://twitter.com/johndoe",
                YouTubeLink = "http://youtube.com/johndoe"
            };

            context.AppUsers.Add(appUser);
            await context.SaveChangesAsync();

            // Seed ProfileImages and BackgroundImages
            var profileImage = new ProfileImage
            {
                Url = "",
                IsMain = true,
                AppUserId = appUser.Id
            };

            var backgroundImage = new BackgroundImage
            {
                Url = "",
                AppUserId = appUser.Id
            };

            context.ProfileImages.Add(profileImage);
            context.BackgroundImages.Add(backgroundImage);
            await context.SaveChangesAsync();
        }

        // Seed Articles
        if (!await context.Articles.AnyAsync())
        {
            var articles = new List<Article>
            {
                new Article
                {
                    Title = "Vestibulum eu leo accumsan, congue erat fermentum, blandit magna.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit...",
                    Headline = "Vivamus porta ligula id nisl mattis, ac ullamcorper erat ultrices.",
                    IsEvent = true,
                    FacebookLink = "http://facebook.com/eventarticle1",
                    YouTubeLink = "https://youtu.be/xk4RK2oGa3c?si=D7LhDY8mzvg4P9uf",
                    Images = new List<Image>
                    {
                        new Image { Url = "https://images.pexels.com/photos/27052153/pexels-photo-27052153/free-photo-of-relax.jpeg?auto=compress&cs=tinysrgb&w=600&lazy=load" },
                        new Image { Url = "https://images.pexels.com/photos/26997896/pexels-photo-26997896/free-photo-of-woman-in-t-shirt-and-skirt-walking-by-field-in-countryside.jpeg?auto=compress&cs=tinysrgb&w=600&lazy=load" }
                    },
                    Videos = new List<Video>
                    {
                        new Video { Url = "https://videos.pexels.com/video-files/3226171/3226171-sd_640_360_24fps.mp4", IsExternal = true }
                    }
                },
                new Article
                {
                    Title = "Aenean quis turpis eget dolor sodales vestibulum sagittis ullamcorper nisi.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam congue lacus urna, ut eleifend lacus faucibus eget. Praesent bibendum commodo metus non luctus. Integer luctus nunc nulla, ut vestibulum dolor tincidunt ut. Nam pharetra velit ut neque tempus, vel rutrum velit congue. Curabitur a ultrices ex. Mauris libero velit, facilisis vitae enim non, rhoncus viverra massa. Phasellus sit amet consequat mi, et varius purus. Maecenas in placerat neque, sed congue sapien. In a ultrices velit. Aenean consequat neque sed magna pretium, at porttitor augue rutrum. Praesent iaculis sodales sem, id tempor elit porttitor ut. Sed placerat dictum nisi, eget sollicitudin.",
                    Headline = "Proin fermentum arcu nec risus condimentum, ut vestibulum dolor suscipit.",
                    IsEvent = true,
                    FacebookLink = "",
                    YouTubeLink = "https://youtu.be/ryQIbcprzNA?si=VkzX-cyrtN10rDm-",
                    Images = new List<Image>
                    {
                        new Image {Url = "https://images.pexels.com/photos/27200209/pexels-photo-27200209/free-photo-of-a-street-with-a-tree-in-bloom-and-people-walking.jpeg?auto=compress&cs=tinysrgb&w=600&lazy=load"},
                    },
                    Videos = new List<Video>()
                },
                new Article
                {
                    Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed et ante orci. Maecenas at convallis purus, a suscipit mi. In hac habitasse platea dictumst. Vivamus tincidunt sapien non tortor sollicitudin",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas lacus nunc, congue quis aliquam vitae, sagittis ut dui. Cras vel nulla accumsan, ullamcorper magna non, efficitur lacus. Suspendisse gravida, quam ut pretium dapibus, nunc lectus dapibus quam, sed consequat mi diam eu nibh. Nullam eleifend nisl diam, in iaculis dui euismod venenatis. Etiam efficitur elementum ipsum, non sagittis neque rutrum a. Vestibulum a tempor justo, eget porttitor diam. Etiam non viverra eros. Morbi in auctor enim, et imperdiet odio. Sed.",
                    Headline = "Proin fermentum arcu nec risus condimentum, ut vestibulum dolor suscipit.",
                    IsEvent = true,
                    FacebookLink = "",
                    YouTubeLink = "https://youtu.be/ryQIbcprzNA?si=VkzX-cyrtN10rDm-",
                    Images = new List<Image>
                    {
                        new Image {Url = "https://images.pexels.com/photos/26976861/pexels-photo-26976861/free-photo-of-rear-view-of-a-woman-wrapped-in-a-white-shawl-standing-on-the-sand-dune.jpeg?auto=compress&cs=tinysrgb&w=600&lazy=load"},
                    },
                    Videos = new List<Video>
                    {
                        new Video { Url = "https://videos.pexels.com/video-files/1093662/1093662-sd_640_360_30fps.mp4", IsExternal = false },
                        new Video { Url = "https://videos.pexels.com/video-files/2759477/2759477-sd_640_360_30fps.mp4", IsExternal = false }
                    }
                },
                new Article
                {
                    Title = "Proin fermentum arcu nec risus condimentum, ut vestibulum dolor suscipit.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit...",
                    Headline = "Proin fermentum arcu nec risus condimentum, ut vestibulum dolor suscipit.",
                    IsEvent = false,
                    FacebookLink = "",
                    YouTubeLink = "https://youtu.be/MG1ErejY3jw?si=4sRDu8fnLtwHxVTG",
                    Images = new List<Image>
                    {
                        new Image { Url = "https://images.pexels.com/photos/18391175/pexels-photo-18391175/free-photo-of-young-woman-in-a-suit-standing-on-the-pavement-in-city.jpeg?auto=compress&cs=tinysrgb&w=600&lazy=load" }
                    },
                    Videos = new List<Video>()
                },
                new Article
                {
                    Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed et ante orci. Maecenas at convallis purus, a suscipit mi. In hac habitasse platea dictumst. Vivamus tincidunt sapien non tortor sollicitudin",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam congue lacus urna, ut eleifend lacus faucibus eget. Praesent bibendum commodo metus non luctus. Integer luctus nunc nulla, ut vestibulum dolor tincidunt ut. Nam pharetra velit ut neque tempus, vel rutrum velit congue. Curabitur a ultrices ex. Mauris libero velit, facilisis vitae enim non, rhoncus viverra massa. Phasellus sit amet consequat mi, et varius purus. Maecenas in placerat neque, sed congue sapien. In a ultrices velit. Aenean consequat neque sed magna pretium, at porttitor augue rutrum. Praesent iaculis sodales sem, id tempor elit porttitor ut. Sed placerat dictum nisi, eget sollicitudin.",
                    Headline = "Regular Headline 2",
                    IsEvent = false,
                    FacebookLink = "",
                    YouTubeLink = "",
                    Images = new List<Image>(),
                    Videos = new List<Video>()
                },
                new Article
                {
                    Title = "Regular Article 2",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit...",
                    Headline = "Regular Headline 2",
                    IsEvent = false,
                    FacebookLink = "",
                    YouTubeLink = "https://youtu.be/x_GA9_Eitic?si=RsoXAdxSv-TRMICW",
                    Images = new List<Image>(),
                    Videos = new List<Video>()
                },
                new Article
                {
                    Title = "Regular Article 2",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit...",
                    Headline = "Regular Headline 2",
                    IsEvent = false,
                    FacebookLink = "",
                    YouTubeLink = "",
                    Images = new List<Image>(),
                    Videos = new List<Video>
                    {
                        new Video {Url ="https://videos.pexels.com/video-files/3126661/3126661-sd_640_360_24fps.mp4", IsExternal = false },
                        new Video {Url ="https://youtu.be/lWo2WLVCHfE?si=sXL50kFjdS9KOHFh", IsExternal = true }
                    }
                },
                new Article
                {
                    Title = "Regular Article 2",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed et ante orci. Maecenas at convallis purus, a suscipit mi. In hac habitasse platea dictums",
                    Headline = "Regular Headline 2",
                    IsEvent = false,
                    FacebookLink = "",
                    YouTubeLink = "https://youtu.be/dlHtjDc1z8E?si=uIRdU6tes4OYvKAD",
                    Images = new List<Image>
                    {
                        new Image { Url ="https://images.pexels.com/photos/27077981/pexels-photo-27077981/free-photo-of-currants-on-a-table.jpeg?auto=compress&cs=tinysrgb&w=600&lazy=load"}
                    },
                    Videos = new List<Video>()
                },
                new Article
                {
                    Title = "Vestibulum eu leo accumsan, congue erat fermentum, blandit magna.",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit...",
                    Headline = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed et ante orci. Maecenas at convallis purus, a suscipit",
                    IsEvent = false,
                    FacebookLink = "",
                    YouTubeLink = "https://youtu.be/dlHtjDc1z8E?si=uIRdU6tes4OYvKAD",
                    Images = new List<Image>
                    {
                        new Image { Url ="https://images.pexels.com/photos/27077981/pexels-photo-27077981/free-photo-of-currants-on-a-table.jpeg?auto=compress&cs=tinysrgb&w=600&lazy=load"}
                    },
                    Videos = new List<Video>
                    {
                        new Video {Url ="https://videos.pexels.com/video-files/3058708/3058708-sd_640_360_24fps.mp4"}
                    }
                }
            };

            context.Articles.AddRange(articles);
            await context.SaveChangesAsync();
        }
    }
}
