namespace DotNext.Net.Cluster.Consensus.Raft;

internal abstract class TokenizedState<TMember>(IRaftStateMachine<TMember> stateMachine) : RaftState<TMember>(stateMachine)
    where TMember : class, IRaftClusterMember
{
    public abstract CancellationToken Token { get; }
}