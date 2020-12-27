# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

from botbuilder.core import ActivityHandler, TurnContext,MessageFactory
from botbuilder.core.teams import TeamsActivityHandler
from botbuilder.schema import ChannelAccount,MessageReaction
from botbuilder.schema.teams import ChannelInfo,TeamInfo
from typing import List


class MyBot(TeamsActivityHandler):
    # See https://aka.ms/about-bot-activity-message to learn more about the message and other activity types.

    async def on_message_activity(self, turn_context: TurnContext):
        await turn_context.send_activity(f"You said '{ turn_context.activity.text }'")

    async def on_members_added_activity(
        self,
        members_added: ChannelAccount,
        turn_context: TurnContext
    ):
        for member_added in members_added:
            if member_added.id != turn_context.activity.recipient.id:
                await turn_context.send_activity("Hello and welcome!")

    async def on_reactions_added(self,message_reaction : List[MessageReaction],turn_context : TurnContext):
        for message_reaction in message_reaction:
            await turn_context.send_activity(f"Added : '{message_reaction.type}'")

    async def on_reactions_removed(self,message_reaction : List[MessageReaction],turn_context : TurnContext):
        for message_reaction in message_reaction:
            await turn_context.send_activity(f"Removed : '{message_reaction.type}'")
    
    async def on_teams_channel_created(self,channel_info:ChannelInfo,team_info:TeamInfo,turn_context:TurnContext):
        await turn_context.send_activity(MessageFactory.text(f"new channel has created {channel_info.name}"))
    
    async def on_teams_channel_deleted(self,channel_info:ChannelInfo,team_info:TeamInfo,turn_context:TurnContext):
         await turn_context.send_activity(MessageFactory.text(f"Channel {channel_info.name} has deleted"))
    
    async def on_teams_channel_renamed(self,channel_info:ChannelInfo,team_info:TeamInfo,turn_context:TurnContext):
        await turn_context.send_activity(MessageFactory.text(f"Chaneel renamed as {channel_info.name}"))
    
    