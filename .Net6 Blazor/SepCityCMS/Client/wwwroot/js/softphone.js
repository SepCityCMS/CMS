$(function () {
    var speakerDevices = document.getElementById('speaker-devices');
    var ringtoneDevices = document.getElementById('ringtone-devices');
    var outputVolumeBar = document.getElementById('output-volume');
    var inputVolumeBar = document.getElementById('input-volume');
    var volumeIndicators = document.getElementById('volume-indicators');
    
    $.getJSON('/twiliotoken')
        .done(function (data) {

            // Setup Twilio.Device
            Twilio.Device.setup(data.token);

            Twilio.Device.ready(function (device) {
                $('#PhoneStatus').html('Device Ready...');
            });

            Twilio.Device.error(function (error) {
                alert('Device Error: ' + error.message);
                $('#PhoneStatus').html('Device Error');
            });

            Twilio.Device.connect(function (conn) {
                $('#PhoneStatus').html('Call established.');
                document.getElementById('button-call').style.display = 'none';
                document.getElementById('button-hangup').style.display = 'inline';
                volumeIndicators.style.display = 'block';
                bindVolumeIndicators(conn);
            });

            Twilio.Device.disconnect(function (conn) {
                $('#PhoneStatus').html('Call ended.');
                document.getElementById('button-call').style.display = 'inline';
                document.getElementById('button-hangup').style.display = 'none';
                volumeIndicators.style.display = 'none';
            });

            Twilio.Device.incoming(function (conn) {
                $('#PhoneStatus').html(conn.parameters.From + ' calling.');
                conn.accept();
            });
            
            Twilio.Device.audio.on('deviceChange', updateAllDevices);

            // Show audio selection UI if it is supported by the browser.
            if (Twilio.Device.audio.isSelectionSupported) {
                document.getElementById('output-selection').style.display = 'block';
            }
        })
        .fail(function () {
            $('#PhoneStatus').html('Device error.');
            alert('Could not get a token from server!');
        });

    // Bind button to make call
    document.getElementById('button-call').onclick = function () {
        // get the phone number to connect the call to
        var params = {
            To: $('#phone-number').intlTelInput("getNumber")
        };
        Twilio.Device.connect(params);
    };

    // Bind button to hangup call
    document.getElementById('button-hangup').onclick = function () {
        $('#PhoneStatus').html('Hanging up...');
        Twilio.Device.disconnectAll();
    };
    
    speakerDevices.addEventListener('change', function () {
        var selectedDevices = [].slice.call(speakerDevices.children)
            .filter(function (node) { return node.selected; })
            .map(function (node) { return node.getAttribute('data-id'); });

        Twilio.Device.audio.speakerDevices.set(selectedDevices);
    });

    ringtoneDevices.addEventListener('change', function () {
        var selectedDevices = [].slice.call(ringtoneDevices.children)
            .filter(function (node) { return node.selected; })
            .map(function (node) { return node.getAttribute('data-id'); });

        Twilio.Device.audio.ringtoneDevices.set(selectedDevices);
    });

    function bindVolumeIndicators(connection) {
        connection.volume(function (inputVolume, outputVolume) {
            var inputColor = 'red';
            if (inputVolume < .50) {
                inputColor = 'green';
            } else if (inputVolume < .75) {
                inputColor = 'yellow';
            }

            inputVolumeBar.style.width = Math.floor(inputVolume * 300) + 'px';
            inputVolumeBar.style.background = inputColor;

            var outputColor = 'red';
            if (outputVolume < .50) {
                outputColor = 'green';
            } else if (outputVolume < .75) {
                outputColor = 'yellow';
            }

            outputVolumeBar.style.width = Math.floor(outputVolume * 300) + 'px';
            outputVolumeBar.style.background = outputColor;
        });
    }

    function updateAllDevices() {
        updateDevices(speakerDevices, Twilio.Device.audio.speakerDevices.get());
        updateDevices(ringtoneDevices, Twilio.Device.audio.ringtoneDevices.get());
    }
});

// Update the available ringtone and speaker devices
function updateDevices(selectEl, selectedDevices) {
    selectEl.innerHTML = '';
    Twilio.Device.audio.availableOutputDevices.forEach(function (device, id) {
        var isActive = (selectedDevices.size === 0 && id === 'default');
        selectedDevices.forEach(function (device) {
            if (device.deviceId === id) { isActive = true; }
        });

        var option = document.createElement('option');
        option.label = device.label;
        option.setAttribute('data-id', id);
        if (isActive) {
            option.setAttribute('selected', 'selected');
        }
        selectEl.appendChild(option);
    });
}
