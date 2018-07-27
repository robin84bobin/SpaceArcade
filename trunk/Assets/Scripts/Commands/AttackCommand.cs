
using System;

public class AttackCommand : ICommand
{
    private ActorInputController _input;

    public AttackCommand(ActorInputController input)
    {
        _input = input;
    }

    public override void Execute()
    {
        _input.Attack();
    }

    public override void Undo()
    {
        //throw new NotImplementedException();
    }
}

