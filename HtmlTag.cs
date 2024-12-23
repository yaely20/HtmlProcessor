using System;
using System.Collections.Generic;
using System.Linq;

public class HtmlTag
{
  public string Id { get; set; }
  public string Name { get; set; }
  public Dictionary<string, string> Attributes { get; set; }
  public List<string> Classes { get; set; }
  public string InnerHtml { get; set; }
  public HtmlTag Parent { get; set; }
  public List<HtmlTag> Children { get; set; }

  public IEnumerable<HtmlTag> Descendants()
  {
    var queue = new Queue<HtmlTag>();
    queue.Enqueue(this);

    while (queue.Count > 0)
    {
      var current = queue.Dequeue();

      yield return current;

      foreach (var child in current.Children)
      {
        queue.Enqueue(child);
      }
    }
  }
  public IEnumerable<HtmlTag> Ancestors()
  {
    HtmlTag current = this.Parent;

    while (current != null)
    {
      yield return current;
      current = current.Parent;
    }

  }
  public List<HtmlTag> FindElements(Selector selector)
  {
    var results = new HashSet<HtmlTag>();
    FindElementsRecursive(this, selector, results);
    return results.ToList();
  }

  private void FindElementsRecursive(HtmlTag htmlTag, Selector selector, HashSet<HtmlTag> results)
  {
    throw new NotImplementedException();
  }

  private void FindElementsRecursive(HtmlTag element, Selector selector, HashSet<HtmlTag> results)
  {
    if (selector == null) return;

    var descendants = element.Descendants();
    var matchingElements = descendants.Where(e => MatchesSelector(e, selector));

    if (selector.Child == null)
    {
      foreach (var match in matchingElements)
      {
        results.Add(match);
      }
      return;
    }

    foreach (var match in matchingElements)
    {
      FindElementsRecursive(match, selector.Child, results);
    }
  }
  private bool MatchesSelector(HtmlTag element, Selector selector)
  {
    return MatchesTag(element, selector) &&
           MatchesId(element, selector) &&
           MatchesClasses(element, selector);
  }

  private bool MatchesTag(HtmlTag element, Selector selector)
  {
    return string.IsNullOrEmpty(selector.TagName) ||
           string.Equals(element.Name, selector.TagName, System.StringComparison.OrdinalIgnoreCase);
  }

  private bool MatchesId(HtmlTag element, Selector selector)
  {
    return string.IsNullOrEmpty(selector.Id) ||
           string.Equals(element.Id, selector.Id, System.StringComparison.OrdinalIgnoreCase);
  }

  private bool MatchesClasses(HtmlTag element, Selector selector)
  {
    return !selector.Classes.Any() ||
           selector.Classes.All(cls => element.Classes.Contains(cls));
  }
}
