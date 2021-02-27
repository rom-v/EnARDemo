using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader
{
    static public Product[] CreateProductsArray(string text)
    {
        string[] lines = text.Split(new char[] { '\n' });
        Debug.Log(lines.Length);

        int idIndex=0;
        int titleIndex=1;
        int imageLinkIndex = 3;
        int textureIndex = 7;

        List<Product> products = new List<Product>();

        for (int i=1; i< lines.Length; i++)
        {
            if (lines[i].Length > 10)
            {
                string[] parts = lines[i].Split(new char[] {','});

                Product product = new Product();
                product.id = parts[idIndex];
                product.title = parts[titleIndex].Replace("\"", "");
                product.imageLink = parts[imageLinkIndex].Replace("\"", "");
                product.texture = parts[textureIndex].Replace("\"", "");

                products.Add(product);
            }
        }

        return products.ToArray();
    }
}

public class Product
{
    public string id;
    public string title;
    public string imageLink;
    public string texture;
}
