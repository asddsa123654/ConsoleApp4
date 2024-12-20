using System;
using System.Collections;
using System.Collections.Generic;

public class Document : IComparable<Document>
{
    public string Title { get; set; }
    public int Pages { get; set; }
    public int Confidentiality { get; set; }

    public Document(string title, int pages, int confidentiality)
    {
        Title = title;
        Pages = pages;
        Confidentiality = confidentiality;
    }

    public int CompareTo(Document other)
    {
        if (other == null)
            return 1;

        return this.Pages.CompareTo(other.Pages);
    }

    public override string ToString()
    {
        return $"Title: {Title}, Pages: {Pages}, Confidentiality: {Confidentiality}";
    }
}

public class DocumentComparer : IComparer<Document>
{
    public int Compare(Document x, Document y)
    {
        if (x == null || y == null)
            throw new ArgumentNullException("Документи не можуть бути null");

        int pageComparison = x.Pages.CompareTo(y.Pages);
        if (pageComparison != 0)
            return pageComparison;

        return x.Confidentiality.CompareTo(y.Confidentiality);
    }
}

public class DocumentCollection : IEnumerable<Document>
{
    private List<Document> documents;

    public DocumentCollection()
    {
        documents = new List<Document>();
    }

    public void Add(Document document)
    {
        documents.Add(document);
    }

    public IEnumerator<Document> GetEnumerator()
    {
        return documents.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public void PrintDocumentsSortedByPages()
    {
        documents.Sort();
        foreach (var document in documents)
        {
            Console.WriteLine(document);
        }
    }
}

class Program
{
    static void Main()
    {
        DocumentCollection documentCollection = new DocumentCollection();

        documentCollection.Add(new Document("Document 1", 50, 2));
        documentCollection.Add(new Document("Document 2", 120, 1));
        documentCollection.Add(new Document("Document 3", 30, 3));
        documentCollection.Add(new Document("Document 4", 50, 4));

        Console.WriteLine("Документи вiдсортованi за кiлькiстю сторнок:");
        documentCollection.PrintDocumentsSortedByPages();

        List<Document> documentList = new List<Document>
        {
            new Document("Doc A", 100, 3),
            new Document("Doc B", 50, 2),
            new Document("Doc C", 100, 1),
            new Document("Doc D", 75, 4)
        };

        documentList.Sort(new DocumentComparer());
        Console.WriteLine("\nДокументи вiдсортованi за сторiнками та конфiденцiйнiстю:");
        foreach (var doc in documentList)
        {
            Console.WriteLine(doc);
        }
    }
}
