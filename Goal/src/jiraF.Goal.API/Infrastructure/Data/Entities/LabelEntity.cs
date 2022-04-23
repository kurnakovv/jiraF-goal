using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jiraF.Goal.API.Infrastructure.Data.Entities;

[Table("label")]
public class LabelEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_label")]
    public Guid Id { get; set; }

    [Column("l_title")]
    public string Title { get; set; }
}
