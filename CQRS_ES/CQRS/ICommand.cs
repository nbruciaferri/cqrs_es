using System;
namespace CQRS_ES.CQRS
{
    public interface ICommand
    {
        void Apply();
    }
}
