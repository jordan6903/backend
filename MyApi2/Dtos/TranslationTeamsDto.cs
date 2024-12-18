﻿namespace MyApi2.Dtos
{
    public class TranslationTeamsDto
    {
        public long Id { get; set; }

        public string P_id { get; set; } = null!;

        public string? P_Name { get; set; }

        public string? P_CName { get; set; }

        public string? Sale_Date { get; set; }

        public string? C_id { get; set; }

        public string? C_Name { get; set; }

        public byte T_batch { get; set; }

        public string? T_id { get; set; }

        public string? T_Name { get; set; }

        public byte Type_id { get; set; }

        public string? Type_Name { get; set; }

        public string? Remark { get; set; }

        public DateTime? Upd_date { get; set; }

        public string? Upd_user { get; set; }

        public DateTime? Create_dt { get; set; }

        public ICollection<TTviewsDto1>? T_batch_data { get; set; }

        public string? T_batch_data_show { get; set; }

        public string? Url_data_show { get; set; }

        public string? Pic_data_show { get; set; }

        public string? Upd_date_show { get; set; }
    }
}
