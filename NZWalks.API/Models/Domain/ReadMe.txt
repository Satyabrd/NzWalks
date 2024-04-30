Here are 3 models or tables
-> Difficulty table:
    -> Id               : type(GUID) : Not nullable
    -> Name             : string     : Not nullable
-> Regions table:
    -> Id               : type(GUID) : Not nullable
    -> Code             : string     : Not nullable
    -> Name             : string     : Not nullable
    -> RegionImageUrl   : string     : Can be Nullable
-> Walks table:
    -> Id               : GUID       : Not nullable
    -> Name             : string     : Not nullable
    -> Description      : string     : Not nullable
    -> LengthInKm       : string     : Not nullable
    -> WalkImageUrl     : string     : Can be Nullable
    -> RegionId         : GUID       : Not nullable (id of region table)
    -> DifficultyId     : GUID       : Not nullable (id of difficulty table)

sql server running in docker:
docker pull mcr.microsoft.com/azure-sql-edge:latest
