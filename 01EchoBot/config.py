#!/usr/bin/env python3
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

import os

class DefaultConfig:
    """ Bot Configuration """

    PORT = 3978
    APP_ID = os.environ.get("MicrosoftAppId", "b54bf997-b017-41ab-92ca-3e516cf18dfb")
    APP_PASSWORD = os.environ.get("MicrosoftAppPassword", "Aev_AJ96~XU-53Zj26rVaFN.rh_t87Wqto")
