mergeInto(LibraryManager.library, {
    // const synth = window.speechSynthesis;
    // const voices = synth.getVoices();

    SpeakInit: function () {
        var msg = new SpeechSynthesisUtterance(" ");

        msg.lang = "ko-KR";
        msg.volume = 1; // 0 to 1
        msg.rate = 1; // 0.1 to 10
        msg.pitch = 1; //0 to 2
        msg.voice = window.speechSynthesis.getVoices()[12];
        window.speechSynthesis.speak(msg);
        msg.addEventListener("end", function (event) {
            alert("메시지 종료");
        });
    },

    Speak: function (str) {
        var msg = new SpeechSynthesisUtterance(UTF8ToString(str));

        msg.lang = "ko-KR";
        msg.volume = 1; // 0 to 1
        msg.rate = 1; // 0.1 to 10
        msg.pitch = 1; //0 to 2
        msg.voice = window.speechSynthesis.getVoices()[12];
        // var voices = window.speechSynthesis.getVoices();
        // for (var i = 0; i < voices.length; i++) {
        //     console.log(i);
        //     console.log(voices[i].name);
        // }
        // utterance.voice = voices.filter(function (voice) {
        //     return voice.name == "Google 한국의";
        // })[0];
        // msg.voice = window.speechSynthesis.getVoices()[index]; // Note: some voices don't support altering params
        // stop any TTS that may still be active
        window.speechSynthesis.cancel();
        window.speechSynthesis.speak(msg);
        msg.addEventListener("end", function (event) {
            alert("메시지 종료");
        });
    },

    StopSpeak: function () {
        window.speechSynthesis.cancel();
    },

    ChangeVoice: function () {},

    HideReadyPlayerMeFrame: function () {
        var rpmContainer = document.getElementById("rpm-container");
        rpmContainer.style.display = "none";
    },
});
