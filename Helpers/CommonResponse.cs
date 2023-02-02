namespace Helpers;
using System.ComponentModel.DataAnnotations;

public class CommonResponse
{
    [Required]
    public bool state { get; set; }

    [Required]
    public string msg { get; set; } = string.Empty;

    public object? data { get; set; }
}