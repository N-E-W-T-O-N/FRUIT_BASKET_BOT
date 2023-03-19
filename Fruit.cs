// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio CoreBot v4.18.1

using System.Collections.Generic;

namespace FruitBasket
{
    public class Fruit
    {
        public  string Selected { get; set; }  //Fruit of your choice
        public int Amount { get; set; }  //Possible

        public readonly static List<string> inventory  = new List<string> { "Mango", "Apple", "Orange", "Coconut","Banana" };
    }
}
