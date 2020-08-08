#!/usr/bin/env python3
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

import os

class DefaultConfig:
    """ Bot Configuration """

    PORT = 3978
    APP_ID = os.environ.get("MicrosoftAppId", "81a4f68b-f995-467b-91c5-22d682b4e5bd")
    APP_PASSWORD = os.environ.get("MicrosoftAppPassword", "u1Q~QW_k8a3vpAcbrys6I9BpL5-~MYfe5x")
