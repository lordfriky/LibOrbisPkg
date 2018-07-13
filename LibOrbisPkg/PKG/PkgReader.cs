﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LibOrbisPkg.PKG
{
  class PkgReader : Util.ReaderBase
  {
    public PkgReader(System.IO.Stream s) : base(true, s)
    {
    }

    public Pkg.Header ReadHeader()
    {
      var hdr = new Pkg.Header();
      s.Position = 0x00;
      hdr.CNTMagic = ReadBytes(4);
      if (!hdr.CNTMagic.Equals(Pkg.Magic))
        throw new Exception("Invalid CNT header");
      s.Position = 0x04;
      hdr.flags = (PKGFlags)UInt();
      s.Position = 0x08;
      hdr.unk_0x08 = UInt();
      s.Position = 0x0C;
      hdr.unk_0x0C = UInt(); /* 0xF */
      s.Position = 0x10;
      hdr.entry_count = UInt();
      s.Position = 0x14;
      hdr.sc_entry_count = UShort();
      s.Position = 0x16;
      hdr.entry_count_2 = UShort(); /* same as entry_count */
      s.Position = 0x18;
      hdr.entry_table_offset = UInt();
      s.Position = 0x1C;
      hdr.main_ent_data_size = UInt();
      s.Position = 0x20;
      hdr.body_offset = ULong();
      s.Position = 0x28;
      hdr.body_size = ULong();
      s.Position = 0x40;
      hdr.content_id = ReadBytes(Pkg.Header.PKG_CONTENT_ID_SIZE); // Length = PKG_CONTENT_ID_SIZE
      s.Position = 0x70;
      hdr.drm_type = UInt();
      s.Position = 0x74;
      hdr.content_type = UInt();
      s.Position = 0x78;
      hdr.content_flags = (ContentFlags)UInt();
      s.Position = 0x7C;
      hdr.promote_size = UInt();
      s.Position = 0x80;
      hdr.version_date = UInt();
      s.Position = 0x84;
      hdr.version_hash = UInt();
      s.Position = 0x88;
      hdr.unk_0x88 = UInt(); /* for delta patches only? */
      s.Position = 0x8C;
      hdr.unk_0x8C = UInt(); /* for delta patches only? */
      s.Position = 0x90;
      hdr.unk_0x90 = UInt(); /* for delta patches only? */
      s.Position = 0x94;
      hdr.unk_0x94 = UInt(); /* for delta patches only? */
      s.Position = 0x98;
      hdr.iro_tag = UInt();
      s.Position = 0x9C;
      hdr.ekc_version = UInt(); /* drm type version */
      s.Position = 0x100;
      hdr.sc_entries1_hash = ReadBytes(Pkg.HASH_SIZE);
      s.Position = 0x120;
      hdr.sc_entries2_hash = ReadBytes(Pkg.HASH_SIZE);
      s.Position = 0x140;
      hdr.digest_table_hash = ReadBytes(Pkg.HASH_SIZE);
      s.Position = 0x160;
      hdr.body_digest = ReadBytes(Pkg.HASH_SIZE);

      // TODO: i think these fields are actually members of element of container array
      s.Position = 0x400;
      hdr.unk_0x400 = UInt();
      s.Position = 0x404;
      hdr.pfs_image_count = UInt();
      s.Position = 0x408;
      hdr.pfs_flags = ULong();
      s.Position = 0x410;
      hdr.pfs_image_offset = ULong();
      s.Position = 0x418;
      hdr.pfs_image_size = ULong();
      s.Position = 0x420;
      hdr.mount_image_offset = ULong();
      s.Position = 0x428;
      hdr.mount_image_size = ULong();
      s.Position = 0x430;
      hdr.package_size = ULong();
      s.Position = 0x438;
      hdr.pfs_signed_size = UInt();
      s.Position = 0x43C;
      hdr.pfs_cache_size = UInt();
      s.Position = 0x440;
      hdr.pfs_image_digest = ReadBytes(Pkg.HASH_SIZE);
      s.Position = 0x460;
      hdr.pfs_signed_digest = ReadBytes(Pkg.HASH_SIZE);
      s.Position = 0x480;
      hdr.pfs_split_size_nth_0 = ULong();
      s.Position = 0x488;
      hdr.pfs_split_size_nth_1 = ULong();
      return hdr;
    }
  }
}
