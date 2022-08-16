using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jiraF.User.API.Infrastructure.Data.Entities;

[Table("user")]
public class UserEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_user")]
    public Guid Id { get; set; }

    [Column("m_date_of_registration")]
    public DateTime DateOfRegistration { get; set; }

    [Column("m_name")]
    public string Name { get; set; }
}
