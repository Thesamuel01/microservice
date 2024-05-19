using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAPI.Domains;

[Table("products")]
public class Product
{
    [Key]
    [DisplayName("cod_sku_pk")]
    public uint SKU { get; set; }

    [Required]
    [StringLength(100)]
    [DisplayName("name")]
    public string Name { get; set; }

    [StringLength(100)]
    [DisplayName("description")]
    public string Description { get; set; }

    [DisplayName("date_update")]
    public DateTime DateUpdate { get; set; }

    public Product(){ }

    public Product(uint sku, string description,DateTime dateUpdate)
    {
        SKU = sku;
        Description = description;
        DateUpdate = dateUpdate;
    }
}
