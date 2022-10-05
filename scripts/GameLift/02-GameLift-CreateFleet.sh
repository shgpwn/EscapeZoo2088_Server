# https://docs.aws.amazon.com/cli/latest/reference/gamelift/create-fleet.html
# Set variables to suit your development environment
FLEET_NAME=BattleFleet
BUILD_ID=build-e7f769ca-285d-44a9-bee2-ec0e6ddc8b96
SERVER_LAUNCH_PATH=BattleServer.exe
EC2_INSTANCE_TYPE=c5.large
FLEET_TYPE=SPOT
LAUNCH_PATH=BattleServer.exe
RUNTIME_CONFIGURATION='ServerProcesses=[
{LaunchPath="C:\Game\BattleServer.exe",Parameters=50001,ConcurrentExecutions=1},
{LaunchPath="C:\Game\BattleServer.exe",Parameters=50002,ConcurrentExecutions=1},
{LaunchPath="C:\Game\BattleServer.exe",Parameters=50003,ConcurrentExecutions=1},
{LaunchPath="C:\Game\BattleServer.exe",Parameters=50004,ConcurrentExecutions=1},
{LaunchPath="C:\Game\BattleServer.exe",Parameters=50005,ConcurrentExecutions=1}
]'
INSTANCE_ROLE_ARN='arn:aws:iam::746460013620:role/mzc-game-gamelift-fleet-role'

aws gamelift create-fleet \
--name "$FLEET_NAME" \
--build-id "$BUILD_ID" \
--ec2-instance-type "$EC2_INSTANCE_TYPE" \
--fleet-type "$FLEET_TYPE" \
--runtime-configuration "$RUNTIME_CONFIGURATION" \
--instance-role-arn "$INSTANCE_ROLE_ARN" \
--ec2-inbound-permissions 'FromPort=50001,ToPort=50005,IpRange=0.0.0.0/0,Protocol=TCP'
