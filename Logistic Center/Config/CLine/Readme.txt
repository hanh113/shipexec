1.0.0.2
2010/04/19
1.修正權限設定(可由function中修改):
  Allow To Change:只可Update
  Full Control:可Insert,Update,Delete,Enabled,Disabled
2.修改FormLoad時去Focus(SetSelectRow),造成資料大時速度很慢的問題
3.增加班別群組欄位,可設定此線的班別

需新增function:SAJET.SJ_PRIVILEGE_DEFINE
需新增Table:SYS_SHIFT_GROUP

alter table sajet.sys_pdline add (SHIFT_GROUP_ID  NUMBER);
alter table sajet.sys_ht_pdline add (SHIFT_GROUP_ID  NUMBER);