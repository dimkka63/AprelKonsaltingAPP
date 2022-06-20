namespace AprelKonsaltingAPP.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.CompilerServices;

    [Table("client")]
    public partial class client : INotifyPropertyChanged
    {
        [Key]
        public int idclients { get; set; }

        [Display(Name = "Фамилия")]
        [StringLength(45), Required]
        public string surname { get; set; }

        [Display(Name = "Имя")]
        [StringLength(45), Required]
        public string firstname { get; set; }
        
        [Display(Name = "Отчество")]
        [StringLength(45), Required]
        public string middlename { get; set; }

        [Display(Name ="Пол")]
        [StringLength(1), Required]
        public string gender { get; set; }

        [Display(Name = "Дата рождения")]
        [Column(TypeName = "date"), Required]
        public DateTime? DOB { get; set; }

        public long? phonenumber { get; set; }

        [Display(Name = "Электронная почта")]
        [StringLength(45), Required]
        public string email { get; set; }


        private byte[] _image;

        public byte[] image
        {
            get { return _image; }
            set { _image = value; ChangeProperty(); }
        }

        public void ChangeProperty([CallerMemberName] string str ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
