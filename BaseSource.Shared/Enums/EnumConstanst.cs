namespace BaseSource.Shared.Enums
{
    public enum GoogleDriveLinkType : byte
    {
        File,
        Folder
    }
    public enum EUploadDirectoryType : byte
    {
        Project
    }
    public enum EUploadFileType : byte
    {
        All = 1,
        Image,
        Video,
        Doc
    }
    public enum VoteStatus : byte
    {
        Up = 1,
        Down = 2,
    }
}
