# check_automationID

Sorry. This page is Japanese only.These files are licensed by Pronama LLC.

 * check_automationID\Resources\pronama_result.png
 * check_automationID\Resources\pronama_try.png
 * check_automationID\Resources\puronama_normal.png

    https://kei.pronama.jp/guideline-others/

* You can try this free software, but you will need to take full responsibility for your action.

## ����ȂɁH

UIAutomation���g���ƁAC#��VB���邢��PowerShell��Windows�v���O�������������s���鎖���\�ł��B�{�c�[�����g�����Ƃł��̎������ɕK�v�ƂȂ��񂪎��W�ł��܂��B�����葁�������ƁAUIAutomationSpy�̒���򉻔łł�w

* UIAutomation �{�Ƃ̐���

    https://docs.microsoft.com/ja-jp/dotnet/framework/ui-automation/ui-automation-overview

����������s���A�Ⴆ�΃{�^�����N���b�N���������ꍇ�ɂ́A����AutomationId���K�v�ɂȂ�ꍇ������܂��B���������R�[�h�������ꍇ�ł��ˁB

```cs
var btn = FindElementById(mainForm, _TAREGT_ID)
    .GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
```

�{�c�[���ł͏�̃R�[�h�ł��� _TARGE_ID �̏����擾���鎖���o���܂��B

## ����m�F���Ȃ�

�ȉ��̊��ł����m�F�͂��Ă��܂���B�B

* Windows10 Professional(64bit)
* VisualStdio2019

��K�v�Ȃ��̂͒��ӓ_�Ƃ��܂��āB

* �}�C�N���K�v�ł��i�R���^�i���񂪎g���銴���ɂ��Ă��炦��Α������v�j

## �r���h���@

* �܂���clone����Ȃ�zip���𓀂���Ȃ肵�āA�K���ȃt�H���_�ɓW�J���ĉ������B
* �ŁA�r���h�����check_automationID.exe���o���܂��iSpeech�n�Ɋւ���Warning����ʂɏo�܂������݂܂�����u���Ă܂��c�j
* �K�v�ȃt�@�C���\���͈ȉ��Ȋ����ł��B�Ȃ̂�check_automationID.exe��Resources�t�H���_����ׂĎg���ĉ������B

```

check_automationID.exe
��
����Resources
        pronama_result.png
        pronama_try.png
        puronama_normal.png

```

## �g���ۂ̒���

�E�B���X�΍�\�t�g�������Ă���ƁA�ŏ��̋N�����x��������A�ŏ����������F���Ɏ��Ԃ�������Ƃ�����悤�ł��̂ŁA����̓���͐F�X�Ƒ҂��Ă��炦��Ɨǂ������ł��B

��Ǘ��Ҍ����̕K�v�ȃA�v���͂��̂܂܂��Ƃ����炭�_���ł��B�E�N���b�N�u�Ǘ��҂Ƃ��Ď��s�v���K�v�ł��B

## �g����

* �܂��̓A�v�����N������Ƃ���ȉ�ʂ��ł܂��B

![Image](readme_pic/app1.png)

* �f�[�^�̎����͂Q����ł��B

    1. �}�E�X���ړ����ĉ����Ŏw���B�ړ�������A�u�L���b�`�v�Ƌ��т܂���
    1. �u5�b��ߔ��v�{�^�����N���b�N��A�Y���ӏ��Ƀ}�E�X���ړ��B5�b���炢��Ƀf�[�^�����ɂ����܂��B����܂łɈړ������������ĉ������c

* �f�[�^�����ƈȉ��̂悤�ȉ�ʂɐ؂�ւ��܂��B����͓d��́u�T�v�̏��Ƀ}�E�X���ړ������Ď擾�����f�[�^�ł��B�}�ɂ����񂪏o�܂��B�܂��ʒu���i�}��Pos�j���o���Ă܂��̂ŁA�������ʒu���w�肵�ĉ����������ꍇ�͎Q�l�ɂȂ邩���ł��B

![Image](readme_pic/app2.png)

* ���̃f�[�^�͕ۑ��Ȃ����A�j���ł��܂��B�܂��A���̎葱�������Ȃ��Ǝ��̃f�[�^����邱�Ƃ͏o���܂���B
* ������̏ꍇ���u�ۑ��i�N���A�j�v�̃{�^�����N���b�N���ĉ������B�ۑ��t�@�C���̑I����ʂ��o�܂��B
* C�����ɕ\�����ꂽ�e�L�X�g�f�[�^��ۑ����܂��̂ŁA�K���ȃt�@�C�����œK���ȃt�H���_�ɕۑ����ĉ������B
* �uElement�̃t�@�C����ۑ�����v�i�ۑ��t�@�C���̑I����ʁj�ŃL�����Z�����N���b�N����΃f�[�^�͕ۑ������ɃN���A���܂��B
* ���̑�����I����ƁA�N������̏�Ԃɖ߂��ĈႤ�p�[�c��ID�����ɍs�����Ƃ��\�ł��B

���Ȃ݂ɏ�}�̃f�[�^��ۑ�����Ƃ��������e�L�X�g�t�@�C�����o���オ��܂��B

```cs
string AutomationId = "num5Button";
string FrameworkId = "XAML";
int ProcessId = 27688;
string LocalizedControlType = "button";
string Name = "5";
string HelpText = "";
int pX = 1022;
int pY = 784;
```

�Ȃ̂ŁA�Ⴆ��AutomationId���g���ă{�^�����N���b�N�������ƂȂ�ƁA���������R�[�h�������Ηǂ��Ƃ������ɂȂ�̂ł��B

```cs
// �u�T�v�̃{�^�������N���b�N����
string AutomationId = "num5Button";
var btn = FindElementById(mainForm, AutomationId)
    .GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
btn.Invoke();
```

## �Q�l�ɂ����Ē������y�[�W

�ȉ��̏�񂪂ƂĂ��ƂĂ��Q�l�ɂȂ�܂����B���ӂ������܂��B

* https://tercel-tech.hatenablog.com/entry/2015/04/29/181723
* https://qiita.com/harmegiddo/items/afb8ffb65156d3e9fd84


## ���C�Z���X�n�ɂ���

�ȉ��̉摜�̓v��������񗘗p�K�C�h���C���Ɋ�Â����p���K�v�ł��̂ł����ӂ��������B

* check_automationID\Resources\pronama_result.png
* check_automationID\Resources\pronama_try.png
* check_automationID\Resources\puronama_normal.png

�K�C�h���C���͈ȉ��B�t���[�p�r�ł͊��e�ł����g���ۂɂ͈ꉞ�ڂ�ʂ��ĉ������B

https://kei.pronama.jp/guideline-others/

�R�[�h�̃��C�Z���X�͈ꉞMIT�Ƃ����ĉ������B

## PS

* ���R�ł����g�p�����ۂ̑��Q�͒N�����������Ă���܂���B�����������ӂŁB
* ���A���i�͑g�ݍ��݂�C�n����Ă܂��̂�C#�͑f�l����т𔲂������炢�ł��̂ŁA������남�ڂ��ڂ��Ղ���΍K���ł��B

