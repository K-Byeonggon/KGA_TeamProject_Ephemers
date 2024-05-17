using System.Collections.Generic;

//children�� Node�� ������� �����ؾ� �Ҷ� ���
public class SequenceNode : CompositeNode
{
    public SequenceNode(List<Node> children) : base(children) { }

    public override State Evaluate()
    {
        bool anyChildRunning = false;

        foreach (var child in children)
        {
            switch (child.Evaluate())
            {
                case State.RUNNING:
                    anyChildRunning = true;
                    continue;
                case State.SUCCESS:
                    continue;
                case State.FAILURE:
                    state = State.FAILURE;
                    return state;
            }
        }

        state = anyChildRunning ? State.RUNNING : State.SUCCESS;
        return state;
    }
}