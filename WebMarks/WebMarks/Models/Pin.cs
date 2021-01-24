using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace PinBoard.Models
{
    public class Pin
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string SiteName { get; set; }

        public string Tags { get; set; }
        public bool HasData { get; set; }
        public IdentityUser User { get; set; }

        public Board Board { get; set; }


        public Pin(string url)
        {
            this.Url = url;
            this.HasData = false;
        }

        public Pin()
        {
            this.Url = Url;
            this.Image = Image;
            this.Title = Title;
            this.Description = Description;
            this.SiteName = SiteName;
            this.Tags = Tags;
            this.Board = Board;
            this.User = User;
        }

        public void GetMeta(string url) {

            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)) {
                url = "http://" + url;
            }
                var website = new HtmlWeb();
                HtmlDocument document = website.Load(url);
                var metaTags = document.DocumentNode.SelectNodes("//meta");
               
                if (metaTags != null)
                {
                    int matchCount = 0;
                    foreach (var tag in metaTags)
                    {
                        var tagName = tag.Attributes["name"];
                        var tagContent = tag.Attributes["content"];
                        var tagProperty = tag.Attributes["property"];
                        if (tagName != null && tagContent != null)
                        {
                            switch (tagName.Value.ToLower())
                            {
                                case "title":
                                    this.Title = tagContent.Value;
                                    matchCount++;
                                    break;
                                case "description":
                                    this.Description = tagContent.Value;
                                    matchCount++;
                                    break;
                                case "twitter:title":
                                    this.Title = string.IsNullOrEmpty(this.Title) ? tagContent.Value : this.Title;
                                    matchCount++;
                                    break;
                                case "twitter:description":
                                    this.Description = string.IsNullOrEmpty(this.Description) ? tagContent.Value : this.Description;
                                    matchCount++;
                                    break;
                                case "keywords":
                                    this.Tags = tagContent.Value;
                                    matchCount++;
                                    break;
                                case "twitter:image":
                                    this.Image = string.IsNullOrEmpty(this.Image) ? tagContent.Value : this.Image;
                                    matchCount++;
                                    break;
                            }
                        }
                        else if (tagProperty != null && tagContent != null)
                        {
                            switch (tagProperty.Value.ToLower())
                            {
                                case "og:title":
                                    this.Title = string.IsNullOrEmpty(this.Title) ? tagContent.Value : this.Title;
                                    matchCount++;
                                    break;
                                case "og:description":
                                    this.Description = string.IsNullOrEmpty(this.Description) ? tagContent.Value : this.Description;
                                    matchCount++;
                                    break;
                                case "og:image":
                                    this.Image = string.IsNullOrEmpty(this.Image) ? tagContent.Value : this.Image;
                                    matchCount++;
                                    break;
                            }
                        }
                    }
                    this.HasData = matchCount > 0;



                }
              
            
        }

       
       
    }
}
