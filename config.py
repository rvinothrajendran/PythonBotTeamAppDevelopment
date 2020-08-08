#!/usr/bin/env python3
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

import os

class DefaultConfig:
    """ Bot Configuration """

    PORT = 3978
    APP_ID = os.environ.get("MicrosoftAppId", "73d7ab56-2dc9-4009-8dff-d591f83521d1")
    APP_PASSWORD = os.environ.get("MicrosoftAppPassword", "B9q2Xf8-vZc--i.6pvEOyJ.0Y3r2q54G.m")
