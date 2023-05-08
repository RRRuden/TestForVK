using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[Index(nameof(Login), IsUnique = true)]
public class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key] public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public DateTime CreatedTime { get; set; }

    [ForeignKey("UserGroup")] public int UserGroupId { get; set; }
    public UserGroup UserGroup { get; set; }

    [ForeignKey("UserState")] public int UserStateId { get; set; }
    public UserState UserState { get; set;}
}