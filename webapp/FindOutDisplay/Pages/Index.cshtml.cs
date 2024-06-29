using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FindOutDisplay.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public string Word1 {get;set;}
    public string Word2{get;set;}
    public string Word3{get;set;}
    public string Word4{get;set;}
    public string Word5{get;set;}
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        
    }
}
