using System.Collections.Generic;
using System.Collections.Immutable;

namespace Net.Zxnn.Dnd.Core;

public class ImmutableProficiencies : IProficiencies
{
    ISet<string> _armor = null;
    ISet<string> _weapons = null;
    ISet<string> _tools = null;
    ISet<string> _savingThrows = null;
    ISet<string> _skills = null;

    public ISet<string> Armor {
        get {
            if (_armor == null) {
                _armor = ImmutableHashSet<string>.Empty;
            }

            return _armor;
        }

        init {
            _armor = value;
        }
    }

    public ISet<string> Weapons {
        get {
            if (_weapons == null) {
                _weapons = ImmutableHashSet<string>.Empty;
            }

            return _weapons;
        }

        init {
            _weapons = value;
        }
    }

    public ISet<string> Tools {
        get {
            if (_tools == null) {
                _tools = ImmutableHashSet<string>.Empty;
            }

            return _tools;
        }

        init {
            _tools = value;
        }
    }

    public ISet<string> SavingThrows {
        get {
            if (_savingThrows == null) {
                _savingThrows = ImmutableHashSet<string>.Empty;
            }

            return _savingThrows;
        }

        init {
            _savingThrows = value;
        }
    }
    
    public ISet<string> Skills {
        get {
            if (_skills == null) {
                _skills = ImmutableHashSet<string>.Empty;
            }

            return _skills;
        }

        init {
            _skills = value;
        }
    }
}