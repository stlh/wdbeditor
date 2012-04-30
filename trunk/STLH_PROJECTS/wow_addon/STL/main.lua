local _, uf = ...

local function colorString(r, g, b)
    return string.format("|cff%02x%02x%02x", r*255, g*255, b*255)
end

local frameBackdrop  = {
    bgFile = "Interface\\DialogFrame\\UI-DialogBox-Background",  
    edgeFile = "Interface\\AddOns\\STL\\Media\\glow.tga",
    edgeSize = 5,
    insets = {
        left = 3,
        right = 3,
        top = 3,
        bottom = 3,
    },
    color = {
        r = 0,
        g = 0,
        b = 0,
        a = 1,
    },
}

local barBgTexture = {
    bgFile = "Interface\\AddOns\\STL\\Media\\gradient.tga",  
}

local Layouts = {
    player = {
        point = "TOPLEFT",
        parentFrame = UIParent,
        refPoint = "CENTER",
        x = -512,
        y = -128,
        width = 256,
        height = 56,
        healthBarHeight = 38,
        powerBarHeight = 8,
    },
    target = {
        point = "TOPLEFT",
        parentFrame = UIParent,
        refPoint = "CENTER",
        x = 256,
        y = -128,
        width = 256,
        height = 56,
        healthBarHeight = 38,
        powerBarHeight = 8,
    },
    targettarget = {
        point = "TOPLEFT",
        parentFrame = UIParent,
        refPoint = "CENTER",
        x = 100,
        y = -128,
        width = 128,
        height = 48,
        healthBarHeight = 30,
        powerBarHeight = 8,
    },
}

local function healthUpdate(frame)
    local healthText = frame.healthText
    local hp = UnitHealth(frame.unit)
    --hp = hp / UnitHealthMax(unit) * 100
    healthText:SetText(hp)
    healthBar = frame.healthBar
    healthBar:SetValue(hp / UnitHealthMax(frame.unit) * 100)
end

local function powerUpdate(frame)
    local powerText = frame.powerText
    local pl = UnitPower(frame.unit)
    powerText:SetText(pl)
    powerBar = frame.powerBar
    powerBar:SetValue(pl / UnitPowerMax(frame.unit) * 100)
end

local function getUnitClassAndColor(frame)
    frame.class, frame.classFileName = UnitClass(frame.unit)
    frame.classColor = RAID_CLASS_COLORS[frame.classFileName]
    frame.powerType, frame.powerToken = UnitPowerType(frame.unit)
    frame.powerColor = PowerBarColor[frame.powerToken]
    
    frame.healthBar:SetStatusBarColor(frame.classColor.r, frame.classColor.g, frame.classColor.b)
    frame.healthText:SetTextColor(frame.classColor.r, frame.classColor.g, frame.classColor.b, 1)
    frame.powerBar:SetStatusBarColor(frame.powerColor.r, frame.powerColor.g, frame.powerColor.b)
    frame.powerText:SetTextColor(frame.powerColor.r, frame.powerColor.g, frame.powerColor.b, 1)
end

local function onEvent(frame, eventName)
    if "UNIT_HEALTH_FREQUENT" == eventName then
        healthUpdate(frame)
    elseif "UNIT_POWER" == eventName then
        powerUpdate(frame)
    elseif "PLAYER_TARGET_CHANGED" == eventName then
        if UnitExists(frame.unit) then
            if not frame:IsShown() then
                frame:Show()
            end
            getUnitClassAndColor(frame)
            healthUpdate(frame)
            powerUpdate(frame)
        else
            frame:Hide()
        end
    end
end

local function setLayout(frame)
    --print("set layout "...frame.unit)
    layout = Layouts[frame.unit]
    frame:SetPoint(layout.point, layout.parentFrame, layout.refPoint, layout.x, layout.y)
    frame:SetWidth(layout.width);
    frame:SetHeight(layout.height);
    frame:SetBackdrop(frameBackdrop)
    frame:SetBackdropBorderColor(0, 0, 0, 1)
       
    -- Health Bar
    frame.healthBar = CreateFrame("StatusBar", nil, frame)
    frame.healthBar.bg = frame.healthBar:CreateTexture(nil, "BORDER")
    frame.healthBar.bg:SetTexture(barBgTexture)
    frame.healthBar.bg:SetAlpha(1)
    frame.healthBar:SetBackdrop(barTexture)
    frame.healthBar:SetStatusBarTexture("Interface\\AddOns\\STL\\Media\\gradient.tga")
    frame.healthBar:SetWidth(layout.width - 10)
    frame.healthBar:SetHeight(layout.healthBarHeight)
    frame.healthBar:SetPoint("TOPLEFT", frame, "TOPLEFT", 5, -5)
    frame.healthBar:SetMinMaxValues(0, 100)
    frame.healthBar:SetValue(100)
    frame.healthBar.highlight = frame.healthBar:CreateTexture(nil, "OVERLAY")
    frame.healthBar.highlight:SetAllPoints(frame.healthBar)
    frame.healthBar.highlight:SetTexture("Interface\\AddOns\\STL\\Media\\highlightTex.tga")
    frame.healthBar.highlight:SetVertexColor(1, 1, 1, 0.1)
    frame.healthBar.highlight:SetBlendMode("ADD")
    --frame.healthBar:SetBorder(borderTexture)
    -- Power Bar
    frame.powerBar = CreateFrame("StatusBar", nil, frame)
    frame.powerBar:SetStatusBarTexture("Interface\\AddOns\\STL\\Media\\Minimalist.tga")
    frame.powerBar:SetWidth(layout.width - 10)
    frame.powerBar:SetHeight(layout.powerBarHeight)
    frame.powerBar:SetPoint("TOPLEFT", frame.healthBar, "BOTTOMLEFT", 0, 0)
    frame.powerBar:SetMinMaxValues(0, 100)
    frame.powerBar:SetValue(100)
    frame.powerBar.bg = frame.healthBar:CreateTexture(nil, "BORDER")
    frame.powerBar.bg:SetTexture(barBgTexture)
    frame.powerBar.bg:SetAlpha(1)
    -- Health Text
    frame.healthText = frame:CreateFontString(nil)
    frame.healthText:SetFont("Interface\\AddOns\\STL\\Fonts\\Hiragino_Sans_GB_W3.ttf", 26)
    frame.healthText:SetPoint("TOPLEFT", frame, "BOTTOMLEFT", 0, 1)
    frame.healthText:SetWidth(layout.width)
    frame.healthText:SetJustifyH("RIGHT")
    -- Power Text
    frame.powerText = frame:CreateFontString(nil)
    frame.powerText:SetFont("Interface\\AddOns\\STL\\Fonts\\Hiragino_Sans_GB_W3.ttf", 26)
    frame.powerText:SetPoint("TOPLEFT", frame.healthText, "BOTTOMLEFT", 0, 1)
    frame.powerText:SetWidth(layout.width)
    frame.powerText:SetJustifyH("RIGHT")
end

local function init()
    -- Player
    uf.p = CreateFrame("Frame", "StlPlayerUnitFrame", UIParent)
    uf.p:RegisterEvent("UNIT_HEALTH_FREQUENT")
    uf.p:RegisterEvent("UNIT_POWER")
    uf.p:SetScript("OnEvent", onEvent)
    uf.p.unit = "player"
    uf.p.class = nil
    setLayout(uf.p)
    
    -- Target
    uf.t = CreateFrame("Frame", "StlTargetUnitFrame", UIParent)
    uf.t:RegisterEvent("UNIT_HEALTH_FREQUENT")
    uf.t:RegisterEvent("UNIT_POWER")
    uf.t:RegisterEvent("PLAYER_TARGET_CHANGED")
    uf.t:SetScript("OnEvent", onEvent)
    uf.t.unit = "target"
    setLayout(uf.t)
    uf.t:Hide()
    
    uf.tt = CreateFrame("Frame", "StlTargetTargetUnitFrame", UIParent)
    uf.tt:RegisterEvent("UNIT_HEALTH_FREQUENT")
    uf.tt:RegisterEvent("UNIT_POWER")
    uf.tt:RegisterEvent("PLAYER_TARGET_CHANGED")
    uf.tt:SetScript("OnEvent", onEvent)
    uf.tt.unit = "targettarget"
    setLayout(uf.tt)
    uf.tt:Hide()
end

init()
getUnitClassAndColor(uf.p)
healthUpdate(uf.p)
powerUpdate(uf.p)