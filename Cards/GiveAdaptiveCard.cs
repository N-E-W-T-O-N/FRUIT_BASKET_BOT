using Microsoft.Bot.Schema;
using System;
using AdaptiveCards;
using Microsoft.Bot.Builder;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using System.Net.Mime;

namespace FruitBasket.Cards
{

    public class Card
    {


        /*public static Attachment GiveAdaptiveCard(string fruit, string msg)
        {

            string fruit_url = "";
            switch (fruit)
            {
                case "Mango": fruit_url = @"https://wallpapers.com/images/featured-full/mango-pictures-evb0z302mlfebdo0.jpg"; break;
                case "Apple": fruit_url = @"https://wallpapers.com/images/file/apple-fruit-waxy-skin-g31fe788k5m7puab.jpg "; break;
                case "Orange": fruit_url = @"https://healthjade.com/wp-content/uploads/2017/10/orange-fruit.jpg"; break;
                case "Coconut": fruit_url = @"https://wallpapers.com/images/file/freshly-sliced-coconut-0e7d2f6c5ssnz0za.jpg"; break;
                case "Banana": fruit_url = @"https://static.standard.co.uk/s3fs-public/thumbnails/image/2016/07/22/07/banana.jpg"; break;
            }
            string welcomeCard = System.IO.File.ReadAllText("C:\\Users\\NewtonMallick\\source\\repos\\MY DOTNET bots\\FruitBasket\\Cards\\welcomeCard.json");
            welcomeCard = welcomeCard.Replace("FRUIT", fruit);
            welcomeCard = welcomeCard.Replace("IMAGE_URL", fruit_url);
            welcomeCard = welcomeCard.Replace("MESSAGE", msg);

            AdaptiveCard adaptiveCard = new AdaptiveCard("1.5");
            // Create an adaptive card from the JSON string
            adaptiveCard = AdaptiveCards.AdaptiveCard.FromJson(welcomeCard).Card;

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = AdaptiveCards.AdaptiveCard.ContentType,
                Content = adaptiveCard,
            }; 
            return adaptiveCardAttachment;

            // Convert the adaptive card to an attachment
            //Attachment attach = new Attachment()
            //{
            //    ContentType = AdaptiveCard.ContentType,
            //    Content = card
            //};


            //return attach;

        }*/

        public static Attachment GiveAdaptiveCard(string fruit, string msg, int amount)
        {

            string fruit_url = "";
            switch (fruit)
            {
                case "Mango": fruit_url = @"https://wallpapers.com/images/featured-full/mango-pictures-evb0z302mlfebdo0.jpg"; break;
                case "Apple": fruit_url = @"https://wallpapers.com/images/file/apple-fruit-waxy-skin-g31fe788k5m7puab.jpg "; break;
                case "Orange": fruit_url = @"https://healthjade.com/wp-content/uploads/2017/10/orange-fruit.jpg"; break;
                case "Coconut": fruit_url = @"https://wallpapers.com/images/file/freshly-sliced-coconut-0e7d2f6c5ssnz0za.jpg"; break;
                case "Banana": fruit_url = @"https://static.standard.co.uk/s3fs-public/thumbnails/image/2016/07/22/07/banana.jpg"; break;
            }


            var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 0));

            card.Body.Add(new AdaptiveTextBlock(){
                  
                    Size = AdaptiveTextSize.Large,
                    Weight = AdaptiveTextWeight.Bolder,
                    Text = "HERE IS YOUR ORDER :- ",
                    FontType = AdaptiveFontType.Monospace ,
                    Color= AdaptiveTextColor.Dark
            });

            card.Body.Add(new AdaptiveColumnSet()
            {
                Columns = new List<AdaptiveColumn>()
                {

                new AdaptiveColumn()
                {
                    Width=AdaptiveColumnWidth.Auto,
                    Items = new List<AdaptiveElement> ()
                    {
                        new AdaptiveImage()
                        {
                        Url = new Uri(fruit_url)    ,
                        Size = AdaptiveImageSize.Small
                        }
                    }
                },
                new AdaptiveColumn()
                {
                    Width=AdaptiveColumnWidth.Auto,
                    Items = new List<AdaptiveElement> ()
                    {
                        new AdaptiveTextBlock()
                        {
                      
                       Text = msg,
                       Wrap = true,
                       Color = AdaptiveTextColor.Attention
                        }
                    }

                }
            }
            });

            card.Body.Add(new AdaptiveTextBlock()
            {

                Size = AdaptiveTextSize.Small,
                Weight = AdaptiveTextWeight.Lighter,
                Text  = "Your order has been placed successfully. Thank you for choosing us!",
                FontType = AdaptiveFontType.Monospace,
                Color = AdaptiveTextColor.Good
            });

            var attach = new Attachment()
            { 
                ContentType = AdaptiveCard.ContentType ,
                Content = card
            };

            return attach;

        }
    }

}
