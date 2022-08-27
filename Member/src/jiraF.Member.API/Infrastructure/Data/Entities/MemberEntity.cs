using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jiraF.Member.API.Infrastructure.Data.Entities;

[Table("member")]
public class MemberEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_member")]
    public Guid Id { get; set; }

    [Column("m_date_of_registration")]
    public DateTime DateOfRegistration { get; set; }

    [Column("m_name")]
    public string Name { get; set; }
}
