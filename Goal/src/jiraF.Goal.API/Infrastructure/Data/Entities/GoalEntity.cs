using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace jiraF.Goal.API.Infrastructure.Data.Entities;

[Table("goal")]
public class GoalEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id_goal")]
    public Guid Id { get; set; }

    [Column("g_title")]
    public string Title { get; set; }

    [Column("g_description")]
    public string Description { get; set; }

    [Column("g_reporter_id")]
    public Guid ReporterId { get; set; }

    [Column("g_assignee_id")]
    public Guid AssigneeId { get; set; }

    [Column("g_date_of_create")]
    public DateTime DateOfCreate { get; set; }

    [Column("g_date_of_update")]
    public DateTime DateOfUpdate { get; set; }

    [Column("g_label_id")]
    public Guid LabelId { get; set; }
}
