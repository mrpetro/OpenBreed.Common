﻿local function onUpdate(entity, dt)

	local pos = entity:GetPosition()
	local text = "(" .. string.format("%.0f", pos.X) .. ", " .. string.format("%.0f", pos.Y) .. ")"
	entity:SetText(0, text)

end

local function onInit(entity)
    Triggers:OnEmitEntity(
        entity,
        Fire,
        true)
end

Fire = function(entity, args)

    local emiterEntity = Entities:GetById(args.EmittedEntityId)
    local emiterPos = emiterEntity:GetPosition()

    entity:SetPosition(emiterPos.X, emiterPos.Y)

    local john = Entities:GetFirstFound("John")
    local johnPos = john:GetPosition()
    local dx = emiterPos.X - johnPos.X
    local dy = emiterPos.Y - johnPos.Y


    entity:SetThrust(dx , dy)
end

return {
    systemHooks = {
        onUpdate = onUpdate,
        onInit = onInit
    }
}