# SCP:SL Bug Report Bot  

A Discord bot that automatically tracks and posts new bug reports from the [SCP:SL Bug Reporting Board](https://git.scpslgame.com/northwood-qa/scpsl-bug-reporting/-/boards/15?milestone_title=No%20Milestone).  
It fetches issues directly from the GitLab API and shares them in your Discord server for easier monitoring.  

## ✨ Features  
- 🔗 Tracks issues from GitLab in real time  
- 📢 Posts updates to a Discord channel of your choice  
- ⚙️ Configurable polling interval (`Min`/`Max`)  
- 🔒 Permission system for starting/stopping tracking  
- 📝 Optional logging for monitoring bot activity  

## 📦 Requirements

- Discord bot token and application with message and slash command permissions
- Linux host system (Tested on Ubuntu)

## 📖 Commands  

- In case the report is deleted before the bot detects it, you have to stop tracking and start it again with the correct report ID.

| Command       | Description              |
|---------------|--------------------------|
| `/track start <id>` | Starts tracking issues from the SCP:SL Bug Reporting board and posts updates in the configured Discord channel. |
| `/track stop`  | Stops tracking issues. No new updates will be posted until tracking is started again. |

## ⚙️ Instalation

1. Download the bot from the releases.
2. Unzip the bot.
3. Copy the bot to your destination directory. 
4. Change permissions, e.g., (chmod 500).
5. Run the bot for the first time to generate default config.
6. Configure the bot and run it again.

## ⚙️ Configuration  

Edit a `config.json` file in the bot’s root directory with the following structure:  

| Field        | Type    | Description                                                                 |
|--------------|---------|-----------------------------------------------------------------------------|
| `Token`      | String  | Your Discord bot token.                                                     |
| `Guild`      | Ulong   | The Discord server (guild) ID where the bot will run.                       |
| `Channel`    | Ulong   | The Discord channel ID where issue updates will be posted.                  |
| `Permissions`| Dictionary  | Maps users or roles to permissions (`track.start`, `track.stop`, `*.*`).    |
| `DiscordId`  | String  | The permissions in the array will be assigned to provided Id.               |
| `TrackLink`  | String  | GitLab API endpoint for fetching SCP:SL bug reports.                        |
| `ShortLink`  | String  | Short issue link used when posting in Discord.                              |
| `Min`        | Short  | Minimum polling interval in seconds (Min 60s).                              |
| `Max`        | Short  | Maximum polling interval in seconds (Max 32767s).                           |
| `Logging`    | Boolean | Enables (`true`) or disables (`false`) logging of bot activity.             |

## ⚙️ Example

Your `config.json` might look like this (0 - Replace it with correct value):  

```json
{
  "token": "<YOUR_DISCORD_BOT_TOKEN>",
  "guild": 0,
  "channel": 0,
  "Permissions": {
    "0": [
      "track.start",
      "track.stop"
    ],
    "0": [
      "*.*"
    ]
  },
  "TrackLink": "https://git.scpslgame.com/api/v4/projects/75/issues/",
  "ShortLink": "https://git.scpslgame.com/northwood-qa/scpsl-bug-reporting/-/issues/",
  "Min": 60,
  "Max": 60,
  "Logging": true
}
