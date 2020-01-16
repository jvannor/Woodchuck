do

function alarm_posted(code, body, headers)
   print("alarm_posted: " .. code)
end

function alarm()
    print("alarm")
    
    http.post('http://192.168.1.179:1018/doorbell',
        'Content-Type: application/json\r\n',
        '{"hello":"world"}',
        alarm_posted)
end

local count = 0
function tick(timer)
    value = adc.read(0)
    if (count > 0) then
       count = count - 1
    elseif (value > 100) then
       count = 15
       alarm()
    end

    print(value)
end

function connected(config)
    tmr.alarm(0, 1000, tmr.ALARM_AUTO, tick)
end

function main()
    status = wifi.sta.status()
    if (status ~= wifi.STA_GOTIP) then
        wifi.sta.connect(connected)
    else
        connected(nil)
    end
end

main()

end
