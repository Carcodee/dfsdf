using UnityEngine;
// Práctica 
public static class ExtensionMethods
{
    public static StateBase stateBase;
    /// <summary>
    /// Indica si el animator está reproduciendo una anumación.
    /// Si la animación está loopeada solo devolcerá true durante la repdroducción de la primera repetición.
    /// </summary>
    /// <param name="animator"></param>
    /// <returns></returns>
    public static bool IsPlaying(this Animator animator)
    {
        //Obtenemos el estado actual del animator indicando el layer 0 (BaseLayer)
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // Obtenemos el punto de la animación en la que se encuentra en el momento actual.
        float currentTime = stateInfo.length * stateInfo.normalizedTime;
        bool result = currentTime < stateInfo.length;
        return result;
    }
    /// <summary>
    /// Indica si se está reproduciendo la animación indicada.
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateName"></param>
    /// <returns></returns>
    public static bool IsPlaying(this Animator animator, string stateName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return animator.IsPlaying() && stateInfo.IsName(stateName);
    }
    /*public static int GetCurrentFrame(this Animator animator, string animatorStateName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(animatorStateName))
        {
            return -1;
        }
        AnimationClip clipFrames = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        float normalizedTime = stateInfo.normalizedTime;
        float totalFrames = clipFrames.length * clipFrames.frameRate;
        int currentFrames = Mathf.FloorToInt(normalizedTime * totalFrames);
        return currentFrames;
    }*/
    /// <summary>
    /// Método que devuelve el número total de frames que tiene una animación
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateName"></param>
    /// <returns></returns>
    public static int GetTotalFrames(this Animator animator, string stateName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // Si la animación que está repdroduciendo el animator no es la indicada.
        if (!stateInfo.IsName(stateName)) return -1;
        AnimatorClipInfo[] clipInfos = animator.GetCurrentAnimatorClipInfo(0);
        //calculamos el total de frames a través de la duración de la animación en segundos y el framerate.
        int totalFrames = Mathf.FloorToInt(clipInfos[0].clip.length * clipInfos[0].clip.frameRate);
        return totalFrames;
    }
    public static int GetCurrentFrame(this Animator animator, string stateName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // Si la animación que está repdroduciendo el animator no es la indicada.
        if (!stateInfo.IsName(stateName)) return -1;
        int totalFrames = animator.GetTotalFrames(stateName);
        // Calculamos el frame actual multiplicando el total de frames por el tiempo normalizado del state info.
        int currentFrame = Mathf.FloorToInt(totalFrames * stateInfo.normalizedTime);
        return currentFrame;
    }
    /*public static int GetCurrentTotalFrames(this Animator animator, string animatorStateName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName(animatorStateName))
        {
            return -1;
        }
        AnimationClip clipFrames = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        int totalFrames = Mathf.RoundToInt(clipFrames.length * clipFrames.frameRate);
        return totalFrames;
    }*/
}