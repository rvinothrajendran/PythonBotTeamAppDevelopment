#!/usr/bin/env python3
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

import os

class DefaultConfig:
    """ Bot Configuration """

    PORT = 3978
    APP_ID = os.environ.get("MicrosoftAppId", "66f5bdb9-31d8-4302-96d7-a6fdab5536f1")
    APP_PASSWORD = os.environ.get("MicrosoftAppPassword", "VinBotSecretDemo!")
